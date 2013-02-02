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

    App.TextEditor = Ember.ContainerView.extend({
        classNames: ['editor'],
        childViews: [],

        didInsertElement: function () {
            this.pushObject(this.viewing());
        },

        viewing: function () {
            return this.get('value') ? this.get('show') : this.get('empty');
        },
        empty: function () {
            return Ember.View.create({
                classNames: ['muted'],
                template: Ember.Handlebars.compile('Click here to edit...'),
                click: function (event) {
                    this.get('parentView').startEdit();
                    event.stopPropagation();
                }
            });
        }.property(),
        show: function () {
            return Ember.View.create({
                template: Ember.Handlebars.compile('{{view.parentView.value}}'),
                click: function (event) {
                    this.get('parentView').startEdit();
                    event.stopPropagation();
                }
            });
        }.property(),
        editing: function () {
            return Ember.View.create({
                templateName: 'components/editor',
                click: function (event) {
                    event.stopPropagation();
                },
                keyDown: function (event) {
                    if (event.keyCode == 27) {
                        this.get('parentView').close();
                    } else if (event.keyCode == 13) {
                        this.get('parentView').save(this.get('value'));
                    }
                },
                focus: function () {
                    Ember.run.sync();
                    Ember.run.later(this, function () {
                        this.$('input').focus();
                        this.$('input').select();
                    }, 100);
                }
            });
        }.property(),

        startEdit: function () {
            this.removeObject(this.viewing());
            this.set('editing.value', this.get('value'));
            this.pushObject(this.get('editing'));
            this.get('editing').focus();
            openedEditor = this;
        },
        save: function (value) {
            if (this.get('value') != value) {
                this.set('value', value);
            }
            this.close();
        },
        close: function () {
            this.removeObject(this.get('editing'));
            this.pushObject(this.viewing());
            openedEditor = null;
        },
    });

    App.TextAreaEditor = App.TextEditor.extend({
        editing: function () {
            return Ember.View.create({
                templateName: 'components/editorArea',
                click: function (event) {
                    event.stopPropagation();
                },
                keyDown: function (event) {
                    if (event.keyCode == 27) {
                        this.get('parentView').close();
                    } else if (event.keyCode == 13) {
                        this.get('parentView').save(this.get('value'));
                    }
                },
                focus: function () {
                    Ember.run.sync();
                    Ember.run.later(this, function () {
                        this.$('textarea').focus();
                        this.$('textarea').select();
                    }, 100);
                }
            });
        }.property(),
    });

})();
