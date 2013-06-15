function HomeController($scope, $http) {
    // Test beer data
    // TODO: GET /api/beers
    $scope.beers = [
        { name: "Beer 1", style: "Lager", rating: "4.2" },
        { name: "Beer 2", style: "Lager", rating: "4.2" },
        { name: "Beer 3", style: "Lager", rating: "4.2" }
    ];
}