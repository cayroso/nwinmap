'use strict';

const mixin = {
    data() {
        return {
            //axios: instance,
            busy: false,
            enums: {
                customerOrderStatuses: [
                    { id: 1, name: 'Requested' },
                    { id: 2, name: 'Accepted' },
                    { id: 3, name: 'Processing' },
                    { id: 4, name: 'Processed' },
                    { id: 5, name: 'InTransit' },
                    { id: 6, name: 'Delivered' },
                    { id: 7, name: 'Completed' },
                    { id: 8, name: 'Cancelled' }
                ]
            }
        };
    },
    async created() {
        //debugger;
    },
    methods: {

        close() {
            const vm = this;
            const href = atob(vm.rurl) || vm.durl;
            
            vm.$util.href(href);
        },
    }
};

export default mixin;
