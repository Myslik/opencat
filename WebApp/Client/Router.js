App.AuthRoute = Ember.Route.extend({
    authenticatedBinding: Ember.Binding.oneWay('App.user.authenticated'),
    redirect: function () {
        if (!this.get('authenticated')) this.transitionTo('guest');
        return this;
    }
});

App.Router.map(function () {
    this.route('index', { path: '/' });
    this.resource('document', { path: '/documents' }, function () {
        this.route('new');
        this.route('edit', { path: '/:document_id' });
    });
    this.route('guest');
});

App.IndexRoute = App.AuthRoute.extend();
App.GuestRoute = Ember.Route.extend();
