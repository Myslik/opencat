﻿Ember.TextArea.reopen({

    onEvent: 'controlEnter focusOut',

    model: null,

    action: null,

    redo: null,

    bubbles: false,

    focusOut: function (event) {
        sendAction('focusOut', this, event);
    },

    insertNewline: function (event) {
        if (event.ctrlKey) {
            sendAction('controlEnter', this, event);
        }
    },

    cancel: function (event) {
        sendAction('redo', this, event);
    }

});

Ember.TextField.reopen({

    onEvent: 'enter focusOut',

    model: null,

    redo: null,

    focusOut: function (event) {
        sendAction('focusOut', this, event);
    },

    insertNewline: function (event) {
        sendAction('enter', this, event);
    },

    keyPress: function (event) {
        sendAction('keyPress', this, event);
    },

    cancel: function (event) {
        sendAction('redo', this, event);
    }

});

function sendAction(eventName, view, event) {
    var action = Ember.get(view, 'action'),
        redo = Ember.get(view, 'redo'),
        on = Ember.get(view, 'onEvent');

    send = function (action) {
        var controller = Ember.get(view, 'controller'),
            model = Ember.get(view, 'model'),
            bubbles = Ember.get(view, 'bubbles');

        controller.send(action, model, view);

        if (!bubbles) {
            event.stopPropagation();
        }
    };

    if (redo != null && eventName === 'redo') {
        send(redo);
    } else if (action !== null && on.w().contains(eventName)) {
        send(action);
    }
}
