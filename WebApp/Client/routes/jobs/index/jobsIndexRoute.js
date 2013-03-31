App.JobsIndexRoute = Ember.Route.extend({
    events: {
        reload: function () {
            App.Job.find();
        }
    },
    model: function () {
        return App.Job.all();
    },
    renderTemplate: function () {
        this.render({ into: 'application' });
    }
});
