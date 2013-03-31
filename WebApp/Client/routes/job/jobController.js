App.JobController = Ember.ObjectController.extend({
    didChanged: function () {
        if (this.get('content.isDirty')) {
            this.get('content').transaction.commit();
        }
    }.observes('content.isDirty')
});
