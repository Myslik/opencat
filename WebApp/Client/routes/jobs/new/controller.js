App.JobsNewController = Ember.ObjectController.extend({

    save: function () {
        var job = App.Job.createRecord(this.get('content'));
        job.save();
        this.transitionToRoute('jobs');
    },

    cancel: function () {
        this.transitionToRoute('jobs');
    }

});
