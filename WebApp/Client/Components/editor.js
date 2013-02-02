(function () {

    "use strict";

    var openedEditor = null;

    $('html').on('click.editor', function (event) {
        if (openedEditor != null) {
            openedEditor.close();
        }
    });

    App.TextEditor = Ember.ContainerView.extend({
        classNames: ['editor'],
        childViews: ['viewing'],
        viewing: function () {
            return Ember.View.create({
                tagNameBinding: 'parentView.viewingTag',
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
            this.removeObject(this.get('viewing'));
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
            this.pushObject(this.get('viewing'));
            openedEditor = null;
        }
    });

})();
