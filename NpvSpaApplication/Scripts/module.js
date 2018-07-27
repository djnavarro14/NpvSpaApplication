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

        var drawChart = function (data) {
            if ($scope.chart) {
                $scope.chart.destroy();
            }

            $scope.chart = new Chart(document.getElementById(model.chartResult), {
                type: 'line',
                data: {
                    labels: data.Labels,
                    datasets: [
                        {
                            label: "NPV",
                            data: data.Values
                        }
                    ]
                },
                options: {
                    legend: { display: false },
                    title: {
                        display: true,
                        text: 'NPV Calculations per Discount Rate'
                    }
                }
            });
        }

        $scope.calculate = function () {
            if (!validateFields()) {
                return;
            }
            
            npvService.post($scope.model).then(function (result) {
                drawChart(result.data)
                $(model.modalResult).modal('show');
            }, function (error) {
                debugger;
                showMessage(error);
            });
        }
    });      

    var showMessage = function (message) { 
        $(model.modalAlert).find(".modal-body").text(message)
        $(model.modalAlert).modal('show');
    }
}