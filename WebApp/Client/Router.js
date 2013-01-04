App.AuthRoute = Ember.Route.extend({
    authenticatedBinding: Ember.Binding.oneWay('App.user.authenticated'),
    redirect: function () {
        if (!this.get('authenticated')) this.transitionTo('guest');
        return this;
    }
});

App.Router.map(function (match) {
    match('/').to('index');
    match('/documents').to('documentsSection', function (match) {
        match('/').to('documents');
        match('/new').to('newDocument');
        match('/:document_id').to('document');
    });
    match('/guest').to('guest');
});

App.IndexRoute = App.AuthRoute.extend();
App.DocumentsRoute = App.AuthRoute.extend({
    model: function () {
        return App.Document.find();
    }
});
App.NewDocumentRoute = App.AuthRoute.extend({
    model: function () {
        return App.Document.createRecord();
    },
    setupControllers: function (controller, model) {
        this.controllerFor('document').set('content', model);
    },
    renderTemplates: function () {
        var controller = this.controllerFor('document');
        this.render({ controller: controller });
    }
})
App.GuestRoute = Ember.Route.extend();
