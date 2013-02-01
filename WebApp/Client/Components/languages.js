App.Languages = Ember.View.extend({
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
    }
});
