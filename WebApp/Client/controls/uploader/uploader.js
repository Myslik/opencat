App.Uploader = Ember.View.extend({
    template: Ember.Handlebars.compile('<i class="icon-upload icon-white"></i> Upload'),
    tagName: 'a',
    classNames: ['btn'],

    // Arguments (id, fileName)
    didUpload: Ember.K,
    // Arguments (id, fileName)
    didCancel: Ember.K,
    // Arguments (id, fileName, responseJSON)
    didComplete: Ember.K,
    // Arguments (id, fileName, errorReason)
    didError: Ember.K,
    // Arguments (id, fileName, uploadedBytes, totalBytes)
    didProgress: Ember.K,

    didActionChanged: function () {
        if (!this.get('url')) return;
        var self = this;
        var uploader = new qq.FileUploaderBasic({
            action: this.get('url'),
            button: this.$().get(0),
            onUpload: function () { self.didUpload.apply(self, arguments); },
            onCancel: function () { self.didCancel.apply(self, arguments); },
            onComplete: function () { self.didComplete.apply(self, arguments); },
            onError: function () { self.didError.apply(self, arguments); },
            onProgress: function () { self.didProgress.apply(self, arguments); }
        });
        this.set('_uploader', uploader);
    }.observes('url'),

    didInsertElement: function () {
        this.didActionChanged();
    }
});

App.UploaderToJob = App.Uploader.extend({
    url: function () {
        if (!this.get('job.id')) return;
        return '/attachments/uploadtojob/%@'.fmt(this.get('job.id'));
    }.property('job.id'),

    didComplete: function () {
        if (this.get('job')) {
            this.get('job').reload();
        }
    }
});
