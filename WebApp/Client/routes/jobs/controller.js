App.JobsIndexController = Ember.ArrayController.extend({
    filteredContent: function () {
        var nameFilter = this.get('nameFilter');
        if (!nameFilter) return this.get('content');
        return this.get('content').filter(function (job) {
            return job.get('name').toLowerCase().indexOf(nameFilter.toLowerCase()) >= 0;
        });
    }.property('nameFilter', 'content')
});

App.JobsNewController = Ember.ObjectController.extend();

App.JobController = Ember.ObjectController.extend({
    didChanged: function () {
        if (this.get('content.isDirty')) {
            this.get('content').transaction.commit();
        }
    }.observes('content.isDirty')
});
