App.Router.map(function () {
    this.resource('jobs', function () {
        this.route('new');
        this.resource('job', { path: '/:job_id' });
    });
    this.route('api');
});
