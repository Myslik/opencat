(function () {

    "use strict";

    var openedEditor = null;

    $('html').on('click.editor', function (event) {
        if (openedEditor != null) {
            openedEditor.close();
        }
    });

    Ember.TextArea.reopen({
        attributeBindings: ['rows', 'cols', 'style']
    });

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
        childViews: ['showView'],

        showView: function () {
            return this.get('value')
                ? ShowView('{{view.parentView.value}}')
                : ShowView('Click here to edit...', ['muted']);
        }.property('value'),

        editView: EditView('components/editor'),

        changeTo: function(state) {
            this.removeAllChildren();
            var view = this.createChildView(this.get(state));
            this.pushObject(view);
            return view;
        },

        edit: function () {
            this.set('editing', this.get('value'));
            this.changeTo('editView').focus();

            if (openedEditor) {
                openedEditor.close();
            }
            openedEditor = this;
        },

        save: function () {
            if (this.get('value') != this.get('editing')) {
                this.set('value', this.get('editing'));
            }
            this.close();
        },
        
        close: function () {
            this.changeTo('showView');
            openedEditor = null;
        }
    });

    App.TextAreaEditor = App.TextEditor.extend({
        editView: EditView('components/editorArea'),
    });

})();
