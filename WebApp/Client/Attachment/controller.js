App.AttachmentsController = Ember.ArrayController.extend({
    removeAttachment: function (attachment) {
        attachment.deleteRecord();
        attachment.store.commit();
    }
});
