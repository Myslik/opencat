App.SimpleLanguages = Ember.View.extend({
    tagName: 'input',
    type: 'hidden',
    prompt: 'Select target languages...',
    attributeBindings: ['tabindex', 'style'],
    style: 'width: 100%;',

    didInsertElement: function () {
        var self = this;
        this.$().select2({
            placeholder: this.get('prompt'),
            multiple: true,
            query: function (query) {
                var data = {};
                data.results = App.Language.all().filter(function (language) {
                    return language.get('name').toLowerCase().indexOf(query.term.toLowerCase()) >= 0;
                }).map(function (language) {
                    return { id: language.get('id'), text: language.get('name') };
                });
                query.callback(data);
            }
        });
        this.$().on('change', function (e) {
            self.set('value', e.val);
        });
    },

    willDestroyElement: function () {
        this.$().select2("destroy");
    }
});

App.ExtendedLangauges = Ember.CollectionView.extend({
    classNames: ['unstyled', 'clearfix', 'languages-extended'],
    tagName: 'ul',

    content: function () {
        return App.Language.all();
    }.property(),

    itemViewClass: Ember.View.extend({
        classNames: ['pull-left'],
        classNameBindings: ['selected'],
        selected: false,
        template: Ember.Handlebars.compile('<label>{{view Ember.Checkbox checkedBinding="view.selected"}} {{view.content.id}}</label>')
    })
});

App.Languages = Ember.ContainerView.extend({
    classNames: ['languages'],
    childViews: ['toggleView', 'simpleView'],

    isExtended: false,

    toggle: function () {
        this.clear();
        this.addView('toggleView');
        this.addView(this.get('isExtended') ? 'simpleView' : 'extendedView');

        this.toggleProperty('isExtended');
    },

    addView: function (name) {
        var view = this.createChildView(this.get(name));
        this.pushObject(view);
        return view;
    },

    toggleView: Ember.View.extend({
        classNames: ['muted'],
        template: Ember.Handlebars.compile('Want to use {{#if view.parentView.isExtended}}simple{{else}}extended{{/if}} version? <a href="javascript: void(0);" {{action toggle target="view.parentView"}}>Toggle</a>')
    }),

    simpleView: App.SimpleLanguages.extend(),
    extendedView: App.ExtendedLangauges.extend()
});


