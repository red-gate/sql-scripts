
FeaturedController.$inject = ['$scope'];

function FeaturedController($scope) {
    var script = function(title, author, description, imgSrc) {
        this.title = title;
        this.author = author;
        this.description = description;
        this.imageSrc = imgSrc || 'http://sqlserverdays.steveverschaeve.be/wp-content/uploads/2014/06/OlaHallengren.jpg';
    }
    $scope.scripts = [
        new script('SQL Server Backup, Integrity Check, Index and Statistics Maintenance', 'Thomas Crossman', ' Solution for Backup, Integrity Check, Index and Statistics Maintenance in SQL Server 2005, SQL Server 2008, SQL Server 2008 R2, SQL Server 2012 and SQL Server 2014.'),
        new script('Finds tables and indexes that do not show up in the usage statistics.', 'Louis Davidson and Tim Ford', ' Finds tables and indexes that do not show up in the usage statistics.', 'http://sqlmag.com/site-files/sqlmag.com/files/uploads/2014/05/tim_ford_2013_sqlspotlight.jpg'),
        new script('Tom is a legend', 'Thomas Crossman', ' is a really long string this is a really long string this is a really long string this is a really long string this is a really long string'),
        new script('Tom is a legend', 'Thomas Crossman', ' is a really long string this is a really long string this is a really long string this is a really long string this is a really long string')
    ];

    $scope.loading.isLoading = true;
    $scope.loading.isLoading = false;
    $scope.loading.pageLoaded = true;
}

module.exports = FeaturedController;
