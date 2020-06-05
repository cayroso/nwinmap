'use strict';

import '../Common/app';
import './app.css';
import './app';

import Vue from 'vue';
import App from './Pages/Shared/app.vue';

import commonPlugin from '../Common/Plugins/commonPlugin';
Vue.use(commonPlugin);

//  global components
import GMap from '../Common/Components/gmap.vue';
import GMapAddItem from '../Common/Components/gmap-add-item.vue';
import Pagination from '../Common/Components/pagination.vue';

Vue.component('g-map', GMap);
Vue.component('g-map-add-item', GMapAddItem);
Vue.component('m-pagination', Pagination);

new Vue({
    el: '#app',
    components: {
        App
    },
    mounted() {
        const vm = this;

    }
});