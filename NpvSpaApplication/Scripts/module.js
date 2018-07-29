function AppViewModel(model) {
    var app = angular.module('npvModule', ["ngRoute"]);

    var config = function ($routeProvider) {
        $routeProvider
            .when("npv/index",
                { templateUrl: "/Npv/Index", controller: "indexController" })
            .otherwise(
                { redirectTo: "/", controller: "indexController" });
    };

    app.config(config);  

    app.service("npvService", function ($http) {
        this.post = function (npvData) {
            var request = $http({
                method: "post",
                url: "/api/values",
                data: npvData
            });
            return request;
        };
    });

    app.controller('indexController', function ($scope, npvService) {
        $scope.model = model;
        
        $scope.add = function () {
            if ($scope.model.CashFlow <= 0) {
                showMessage("Please enter a valid Cash Flow.");
                return;
            }

            $scope.model.CashFlows.push($scope.model.CashFlow);
            $scope.model.CashFlow = 0;
        }

        $scope.delete = function (index) {
            $scope.model.CashFlows.splice(index, 1);
        }

        var validateFields = function () {
            if ($scope.model.InitialInvestment <= 0) {
                showMessage("Please enter a valid Initial Investment.");
                return false;
            }

            if ($scope.model.CashFlows.length === 0) {
                showMessage("Please enter a Cash Flows.");
                return false;
            }

            if ($scope.model.LowerBoundDiscountRate <= 0) {
                showMessage("Please enter a valid Lower Bound Discount Rate.");
                return false;
            }

            if ($scope.model.UpperBoundDiscountRate <= 0) {
                showMessage("Please enter a valid Upper Bound Discount Rate.");
                return false;
            }

            if ($scope.model.DiscountRateIncrement <= 0) {
                showMessage("Please enter a valid Discount Rate Increment.");
                return false;
            }

            return true;
        }
        
        var showChart = function (data) {
            var chart = new CanvasJS.Chart("chartResult", {
                animationEnabled: true,
                title: {
                    text: "NPV per Discount Rate",
                    padding: 10,
                    horizontalAlign: "right"
                },
                axisY: {
                    title: 'NPV',
                    includeZero: false
                },
                axisX: {
                    title: 'Discount Rate',
                    interval: $scope.model.DiscountRateIncrement
                },
                data: [{
                    type: "line",
                    xValueFormatString: "#0.00\"%\"",
                    dataPoints: data
                }]
            });
            chart.render();
            $(model.modalResult).modal('show');
        }

        var extractData = function (result) {
            var data = [];
            result.forEach(function(item) {
                data.push({
                    x: item.data.DiscountRate,
                    y: item.data.NetPresentValue
                });
            });
            return data.sort(function (a, b) {
                return a.x - b.x;
            });
        }

        var round = function (value, decimal) {
            return Number(Math.round(value + 'e' + decimal) + 'e-' + decimal);
        }

        $scope.calculate = function () {
            if (!validateFields()) {
                return;
            }

            var promises = [];
            var discountRate = $scope.model.LowerBoundDiscountRate;
            while (discountRate <= $scope.model.UpperBoundDiscountRate) {
                promises.push(npvService.post({
                    InitialInvestment: $scope.model.InitialInvestment,
                    CashFlows: $scope.model.CashFlows,
                    DiscountRate: discountRate
                }));
                discountRate = round(discountRate + $scope.model.DiscountRateIncrement, 2);
            };

            Promise.all(promises).then(function (result) {
                showChart(extractData(result));
            });
        }
    });      

    var showMessage = function (message) { 
        $(model.modalAlert).find(".modal-body").text(message)
        $(model.modalAlert).modal('show');
    }
}