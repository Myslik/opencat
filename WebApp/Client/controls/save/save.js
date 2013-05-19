(function () {

    App.Save = Ember.View.extend({

        tagName: 'button',
        defaultTemplate: Ember.Handlebars.compile('{{view.title}}'),
        classNames: ['btn', 'btn-success'],
        attributeBindings: ['type', 'disabled'],

        type: 'button',
        disabled: false,
        model: null,
        action: 'save',
        isVisible: false,

        didDirtyChanged: function () {
            if (this.get('model.isDirty')) {
                Ember.run.cancel(this.get('cancel'));
                this.set('title', 'Save');
                this.set('disabled', false);
                this.set('isVisible', true);
            } else {
                this.set('title', 'Saved');
                var cancel = Ember.run.later(this, function () {
                    this.set('isVisible', false);
                }, 2000);
                this.set('cancel', cancel);
            }
        }.observes('model.isDirty'),

        didSavingChanged: function () {
            if (this.get('model.isSaving')) {
                this.set('title', 'Saving...');
                this.set('disabled', true);
                this.set('isVisible', true);
            }
        }.observes('model.isSaving'),

        click: function () {
            if (this.get('disabled')) return;

            var action = this.get('action');
            this.get('controller').send(action);
        }

    });

})();
