App.Languages = Ember.View.extend({
    tagName: 'input',
    type: 'hidden',
    prompt: 'Select target languages...',
    attributeBindings: ['tabindex', 'style'],
    style: 'width: 100%;',

    loadLanguages: function () {
        var languages;
        $.ajax({
            url: '/api/languages',
            dataType: 'json',
            async: false,
            success: function (data) {
                languages = data.map(function (item) {
                    return { id: item.ietf, text: item.name };
                });
            }
        });
        return languages;
    },

    didInsertElement: function () {
        var self = this;
        this.$().select2({
            placeholder: this.get('prompt'),
            multiple: true,
            data: this.loadLanguages()
        });
        this.$().on('change', function (e) {
            self.set('value', e.val);
        });
    }
});
