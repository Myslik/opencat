App.JobController = Ember.ObjectController.extend({
    save: function() {
        this.get('content').save();
    }
});
