App.AttachmentsController = Ember.ArrayController.extend({
    remove: function (attachment) {
        attachment.deleteRecord();
        attachment.transaction.commit();
    }
});
