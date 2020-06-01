'use strict';

export default {
    props: {
        urlView: String,
        uid: String,
        durl: String,
        rurl: String
    },
    //mixin: [pageMixin],
    data() {
        return {
            filter: {
                criteria: '',
                pageIndex: 1,
                pageSize: 10,
                items: [],
                visible: false
            }
        };
    },
    created() {

    },
    methods: {
        initializeFilter(cache) {
            const filter = this.filter;

            const urlParams = new URLSearchParams(window.location.search);

            filter.pageIndex = parseInt(urlParams.get('p'), 10) || 1;
            filter.pageSize = parseInt(urlParams.get('s'), 10) || cache.pageSize || 10;
            filter.sortField = urlParams.get('sf') || cache.sortField || '';
            filter.sortOrder = parseInt(urlParams.get('so'), 10) || cache.sortOrder || -1;
            filter.visible = cache.visible || false;

        },
        async search(index) {
            const vm = this;

            if (index > 0) {
                vm.filter.pageIndex = index;
            }

            await vm._search();
        },
        async search_internal(url, query, state, successAction, failAction, finalAction) {
            const vm = this;

            try {
                await vm.$util.axios(`${url}/${query}`)
                    .then(resp => {
                        vm.filter = Object.assign(vm.filter, resp.data);
                        
                        vm.$util.pushState(query, state, `${vm.durl}${query}`);

                        successAction();
                    });
            } catch (e) {
                failAction();
                vm.$util.handleError(e);
            } finally {
                finalAction();
                const topElem = document.getElementById('top');
                if (topElem) {
                    vm.$nextTick(() => {
                        topElem.scrollIntoView({ behavior: 'smooth' });
                    });
                }
            }
        },
        getRowNumber(index) {
            const filter = this.filter;
            const rowNum = ((filter.pageIndex - 1) * filter.pageSize) + (index + 1);
            return rowNum;
        }
    }
}