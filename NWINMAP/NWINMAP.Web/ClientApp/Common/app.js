'use strict';

import 'bootstrap';
import Vue from 'vue';
import MomentVue from 'vue-moment';
import {
    ModalPlugin, FormSelectPlugin, PaginationPlugin, AlertPlugin, ToastPlugin, TooltipPlugin, CollapsePlugin, BFormGroup, FormInputPlugin,
    FormCheckboxPlugin, OverlayPlugin, FormSpinbuttonPlugin, FormDatepickerPlugin, FormFilePlugin, SidebarPlugin, ImagePlugin, PopoverPlugin,
    CarouselPlugin, NavbarPlugin
} from 'bootstrap-vue';
Vue.use(ModalPlugin);
Vue.use(FormSelectPlugin);
Vue.use(PaginationPlugin);
Vue.use(AlertPlugin);
Vue.use(ToastPlugin);
Vue.use(TooltipPlugin);
Vue.use(CollapsePlugin);
Vue.component('b-form-group', BFormGroup);
Vue.use(FormInputPlugin);
Vue.use(FormCheckboxPlugin);
Vue.use(OverlayPlugin);
Vue.use(FormSpinbuttonPlugin);
Vue.use(FormDatepickerPlugin);
Vue.use(FormFilePlugin);
Vue.use(SidebarPlugin);
Vue.use(ImagePlugin);
Vue.use(PopoverPlugin);
Vue.use(CarouselPlugin);
Vue.use(NavbarPlugin);

Vue.use(MomentVue);

window.moment = require('moment');

Vue.filter('formatDate', function (value) {
    if (value) {
        return moment(String(value)).format('YYYY-MM-DD hh:mm:ss');
    }
});

Vue.filter('toMoment', function (value) {
    if (value) {
        return moment(String(value)).calendar();
    }
});

Vue.filter('toCurrency', function (value) {
    if (typeof value !== "number") {
        return value;
    }
    //var formatter = new Intl.NumberFormat('en-US', {
    //    style: 'currency',
    //    currency: 'USD',
    //    minimumFractionDigits: 2
    //});
    var formatter = new Intl.NumberFormat('en-PH', {
        style: 'currency',
        currency: 'PHP',
        minimumFractionDigits: 2
    });
    return formatter.format(value);
});