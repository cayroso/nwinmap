
    <script>
        'use strict';

        import Vue from 'vue';

        import appMixin from '../../../Common/Mixins/appMixin';

        import Navbar from './navbar.vue';

        import AppIndex from '../index.vue';
        import AdministratorAddEditItem from '../Administrator/add-edit-item.vue';


        export default {
            mixins: [appMixin],
            components: {
                Navbar,

                AppIndex,

                AdministratorAddEditItem
            },
            created() {
                const vm = this;

                //vm.$bus.$on('event:return-bottle', (bottleId) => {
                //    vm.$refs.modalReturnBottle.open(bottleId);
                //});
            },
        }
    </script>