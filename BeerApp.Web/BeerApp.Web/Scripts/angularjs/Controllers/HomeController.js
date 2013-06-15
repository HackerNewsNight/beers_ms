function HomeController($scope, $http) {
    // Test beer data
    // TODO: GET /api/beers
    $scope.beers = [
        { name: "Beer 1" },
        { name: "Beer 2" },
        { name: "Beer 3" }
    ];
}