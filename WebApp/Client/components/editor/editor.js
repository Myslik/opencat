(function () {

    "use strict";

    var ShowView = function (template, classNames) {
        classNames = classNames || [];
        classNames.push('editor-show');
        return Ember.View.extend({
            classNames: classNames,
            template: Ember.Handlebars.compile(template),
            click: function (event) {
                this.get('parentView').edit();
                event.stopPropagation();
            }
        });
    };

    var EditView = function (templateName) {
        return Ember.View.extend({
            templateName: templateName,
            click: function (event) {
                event.stopPropagation();
            },
            keyDown: function (event) {
                if (event.keyCode == 27) {
                    this.get('parentView').close();
                } else if (event.keyCode == 13) {
                    this.get('parentView').save();
                }
            },
            focus: function () {
                Ember.run.sync();
                Ember.run.later(this, function () {
                    this.$('.focusable').focus();
                    this.$('.focusable').select();
                }, 100);
            }
        })
    };

    App.TextEditor = Ember.ContainerView.extend({
        classNames: ['editor'],
        childViews: [],

        showView: ShowView('{{view.parentView.value}} {{#if view.parentView.unsaved}}<span class="message text-warning">You have unsaved changes.</span>{{/if}}'),
        emptyView: ShowView('{{#if view.parentView.unsaved}}<span class="text-warning">You have unsaved changes.</span>{{else}}<span class="muted">Click here to edit...</span>{{/if}}'),
        editView: EditView('components/editor'),

        init: function () {
            this._super();
            this.changeTo(Ember.isEmpty(this.get('value')) ? 'emptyView' : 'showView');
            this.didValueChange();

            this.on('focusOut', this.close);
        },

        changeTo: function(state) {
            this.removeAllChildren();
            var view = this.createChildView(this.get(state));
            this.pushObject(view);
            return view;
        },

        unsaved: function () {
            return this.get('value') != this.get('editing');
        }.property('value', 'editing'),

        didValueChange: function () {
            this.set('editing', this.get('value'));
        }.observes('value'),

        edit: function () {
            this.changeTo('editView').focus();
        },

        save: function () {
            if (this.get('value') != this.get('editing')) {
                this.set('value', this.get('editing'));
            }
            this.close();
        },
        
        close: function () {
            this.changeTo(Ember.isEmpty(this.get('value')) ? 'emptyView' : 'showView');
        }
    });

    App.TextAreaEditor = App.TextEditor.extend({
        editView: EditView('components/editorArea'),
    });

})();
