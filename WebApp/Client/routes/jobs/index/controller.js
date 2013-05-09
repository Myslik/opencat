App.JobsIndexController = Ember.ArrayController.extend({

    reload: function () {
        this.set('content', App.Job.find());
    },

    filteredContent: function () {
        var nameFilter = this.get('nameFilter');
        if (!nameFilter) return this.get('content');
        return this.get('content').filter(function (job) {
            return job.get('name').toLowerCase().indexOf(nameFilter.toLowerCase()) >= 0;
        });
    }.property('nameFilter', 'content')

});
