Ember.TextArea.reopen({

    interpretKeyEvents: function (event) {
        var map = Ember.TextSupport.KEY_EVENTS;
        var method = map[event.keyCode];

        this._elementValueDidChange();
        if (event.ctrlKey && event.keyCode === 13) {
            return sendAction(this, event);
        }
        if (method) { return this[method](event); }
    },

    didInsertElement: function () {
        this.$().tooltip({
            title: 'Ctrl + Enter to save',
            placement: 'bottom',
            trigger: 'focus'
        });
    },
    willDestroyElement: function () {
        this.$().tooltip('destroy');
    },

    action: null,
    bubbles: false

});

Ember.TextField.reopen({

    didInsertElement: function () {
        this.$().tooltip({
            title: 'Enter to save',
            placement: 'bottom',
            trigger: 'focus'
        });
    },
    willDestroyElement: function () {
        this.$().tooltip('destroy');
    }

});

function sendAction(view, event) {
    var action = Ember.get(view, 'action');

    if (action !== null) {
        var controller = Ember.get(view, 'controller'),
            value = Ember.get(view, 'value'),
            bubbles = Ember.get(view, 'bubbles');

        controller.send(action, value, view);

        if (!bubbles) {
            event.stopPropagation();
        }
    }
}
