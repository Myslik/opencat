App.JobsRoute = Ember.Route.extend({

    events: {
        remove: function (job) {
            job.deleteRecord();
            job.save();
            this.transitionTo('jobs');
        }
    }

});
