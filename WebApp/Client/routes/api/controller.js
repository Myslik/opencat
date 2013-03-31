App.ApiController = Ember.Controller.extend({
    resources: ['Job', 'Attachment'],
    current: 'Job',
    
    pluralize: function (name) {
        return name + 's';
    },
    url: function () {
        return Ember.lookup.location.origin + '/api/' + this.pluralize(this.get('current').decamelize());
    }.property('current'),
    modelAttributes: function () {
        var attributes = Ember.get(Ember.lookup, 'App.' + this.get('current') + '.attributes');
        return attributes.keys.list.map(function (key) { return attributes.get(key); });
    }.property('current'),

    select: function (resource) {
        this.set('results', null);
        this.set('current', resource);
    },
    buildUrl: function () {
        var url = this.get('url');
        if (this.get('id')) {
            url = url + '/' + this.get('id');
        }
        return url;
    },
    callGet: function () {
        var self = this;
        self.set('results', null);
        $.ajax({
            type: 'GET',
            url: this.buildUrl(),
            dataType: 'JSON',
            success: function (data, textStatus, jqXHR) {
                self.set('results', JSON.stringify(data, null, 2));
            },
            error: function (jqXHR, textStatus, errorThrown) {
                self.set('results', errorThrown);
            }
        });
    }
});
