App.JobsNewRoute = Ember.Route.extend({
    events: {
        update: function (job) {
            job.transaction.commit();
            this.transitionTo('jobs');
        },
        cancel: function (job) {
            job.deleteRecord();
            this.transitionTo('jobs');
        }
    },
    model: function () {
        return App.Job.createRecord({ name: 'New Job', words: 0 });
    },
    renderTemplate: function () {
        this.render({ into: 'application' });
    }
});
