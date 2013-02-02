App.AuthRoute = Ember.Route.extend({
    authenticatedBinding: Ember.Binding.oneWay('App.user.authenticated'),
    redirect: function () {
        return this;
    }
});

App.Router.map(function () {
    this.route('index', { path: '/' });
    this.resource('jobs', function () {
        this.route('new');
    });
    this.resource('job', { path: '/jobs/:job_id' });
    this.route('api');
});
