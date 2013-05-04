App.JobController = Ember.ObjectController.extend({
    didChanged: function () {
        if (this.get('content.isDirty')) {
            this.get('content').save();
        }
    }.observes('content.isDirty')
});
