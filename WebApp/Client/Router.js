App.AuthRoute = Ember.Route.extend({
    authenticatedBinding: Ember.Binding.oneWay('App.user.authenticated'),
    redirect: function () {
        if (!this.get('authenticated')) this.transitionTo('guest');
        return this;
    }
});

App.Router.map(function (match) {
    match('/').to('index');
    match('/documents').to('documents');
    match('/documents/:document_id').to('document');
    match('/guest').to('guest');
});

App.IndexRoute = App.AuthRoute.extend();
App.DocumentsRoute = App.AuthRoute.extend({
    events: {
        select: function (route, document) {
            route.transitionTo('document', document);
        },
        reload: function (route) {
            App.Document.find();
        },
        remove: function (route, document) {
            var store = document.store;
            document.deleteRecord();
            store.commit();
        }
    },
    model: function () {
        if (App.Document.all().get('length') == 0) {
            return App.Document.find();
        }
        return App.Document.all();
    }
});
App.DocumentRoute = App.AuthRoute.extend({
    events: {
        save: function (route) {
            var controller = route.controllerFor('document');
            controller.get('content').transaction.commit();
            route.transitionTo('documents');
        },
        cancel: function (route) {
            var controller = route.controllerFor('document');
            controller.get('content').transaction.rollback();
            route.transitionTo('documents');
        }
    }
});
App.GuestRoute = Ember.Route.extend();
