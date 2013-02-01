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
    uploadLink: function () {
        if (!this.get('content.id')) return;
        return '/attachments/uploadtodocument/%@'.fmt(this.get('content.id'));
    }.property('content.id')
});
