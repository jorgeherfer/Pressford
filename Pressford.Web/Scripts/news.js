window.pressford = window.pressford || {};
pressford.news = pressford.news || {};

pressford.news.article = function (article) {
    var self = this;
    article = article || {};
    //article.author = article.author || {}
    self.title = ko.observable(article.title);
    self.body = ko.observable(article.body);
    self.articleId = ko.observable(article.articleId);
    self.author = ko.observable(article.author);
    self.comments = ko.observableArray(article.comments);
    self.isUserPublisher = ko.observable(article.isUserPublisher);
    self.isAlreadyLiked = ko.observable(article.isAlreadyLiked);
    self.likeCounter = ko.observable(article.likeCounter);
    self.newComment = ko.observable();
    self.addCommentMode = ko.observable(false);
    self.editMode = ko.observable(false);
    self.editArticle = function () {
        self.editMode(true);
    }
    self.addComment = function () {
        self.addCommentMode(true);
    }
}

pressford.news.viewModel = function () {
    var self = this;
    self.createMode = ko.observable(false);
    self.articles = ko.observableArray();
    self.newArticle = new pressford.news.article();
    self.isUserPublisher = !!document.getElementById("isUserPublisher").value
    self.addArticle = function () {
        self.newArticle.title("");
        self.newArticle.body("");
        self.createMode(true);
    }

    self.cancel = function () {
        self.loadArticles();
    }

    self.cancelCreate = function () {        
    self.createMode(false);
    }

    self.loadArticles = function () {
        $.get("/api/Article").done(function (data) {
            self.articles(ko.utils.arrayMap(data, function (item) {
                return new pressford.news.article(item);
            }));
            self.loadChart();
        });
    }

    self.saveNewArticle = function () {
        $.ajax({
            url: "/api/Article",
            method: "POST",
            datType: "application/json",
            data: { title: self.newArticle.title(), body: self.newArticle.body() }
        }).always(function () {
            self.loadArticles();
        });
        self.createMode(false);
    }

    self.deleteArticle = function (article) {
        $.ajax({
            url: "/api/Article/" + article.articleId(),
            method: "DELETE"
        }).always(function () {
            self.loadArticles();
        });
    }

    self.commentArticle = function (article) {
        $.ajax({
            url: "/api/Article/" + article.articleId,
            method: "PUT",
            datType: "application/json",
            data: article
        }).always(function () {
            self.loadArticles();
        });
    }

    self.saveEditedArticle = function (article) {
        $.ajax({
            url: "/api/Article/" + article.articleId(),
            method: "PUT",
            datType: "application/json",
            data: { articleId: article.articleId(), title: article.title(), body: article.body() }
        }).always(function () {
            self.loadArticles();
        });
    }

    self.saveNewComment = function (article) {
        $.ajax({
            url: "/api/article/addComment/" + article.articleId(),
            method: "POST",
            datType: "application/json",
            data: { '': article.newComment() }
        }).always(function () {
            self.loadArticles();
        });
    }

    self.likeArticle = function (article) {
        $.ajax({
            url: "/api/article/like/" + article.articleId(),
            method: "PUT"
        }).always(function () {
            self.loadArticles();
        });
    }

    self.loadChart = function () {
        var plotData;
        $.get("/api/article/mostLiked").done(function (data) {
            new Chartist.Bar('.ct-chart', {
                labels: $.map(data,function(item){return item.articleTitle}),
                series: $.map(data, function (item) { return item.likeCounter })
            }, {
                distributeSeries: true
            });
        });
    }
}

$(function () {
    var viewModel = new pressford.news.viewModel();
    viewModel.loadArticles();
    ko.applyBindings(viewModel);
});

$.ajaxSetup({
    error: function (jqXHR, testStatus, errorMessage) {
        alert("Error: " + testStatus + ", " + errorMessage);
    }
})