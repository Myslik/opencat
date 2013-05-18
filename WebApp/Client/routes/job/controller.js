App.JobController = Ember.ObjectController.extend({
    save: function () {
        this.get('content').save();
    },
    redo: function () {
        this.get('content').rollback();
    }
});
