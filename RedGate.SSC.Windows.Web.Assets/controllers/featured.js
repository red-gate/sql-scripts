
FeaturedController.$inject = ['$scope'];

function FeaturedController($scope) {
    var script = function(title, author, description) {
        this.title = title;
        this.author = author;
        this.description = description;
        this.imageSrc = '//placehold.it/100x170';
    }
    $scope.scripts = [
        new script('foo', 'bar', 'baz')
    ];

    $scope.loading.isLoading = true;
    $scope.loading.isLoading = false;
    $scope.loading.pageLoaded = true;
}

module.exports = FeaturedController;
