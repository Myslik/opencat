App.DocumentsIndexController = Ember.ArrayController.extend({
    filteredContent: function () {
        var nameFilter = this.get('nameFilter');
        if (!nameFilter) return this.get('content');
        return this.get('content').filter(function (document) {
            return document.get('name').toLowerCase().indexOf(nameFilter.toLowerCase()) >= 0;
        });
    }.property('nameFilter', 'content')
});

App.DocumentsNewController = Ember.ObjectController.extend();

App.DocumentController = Ember.ObjectController.extend({
    didChanged: function () {
        if (this.get('content.isDirty')) {
            this.get('content').transaction.commit();
        }
    }.observes('content.isDirty')
});
