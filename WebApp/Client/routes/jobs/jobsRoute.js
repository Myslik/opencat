App.JobsRoute = Ember.Route.extend({
    events: {
        create: function () {
            this.transitionTo('jobs.new');
        },
        show: function (job) {
            this.transitionTo('job', job);
        },
        remove: function (job) {
            job.deleteRecord();
            job.transaction.commit();
            this.transitionTo('jobs');
        }
    }
});
