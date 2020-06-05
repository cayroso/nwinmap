
    <script>
        'use strict';

        import Vue from 'vue';

        import appMixin from '../../../Common/Mixins/appMixin';

        import Navbar from './navbar.vue';

        import AppIndex from '../index.vue';
        import AdministratorAddEditItem from '../Administrator/add-edit-item.vue';
        import AdministratorUsersIndex from '../Administrator/users/index.vue';

        export default {
            mixins: [appMixin],
            components: {
                Navbar,

                AppIndex,

                AdministratorAddEditItem,

                AdministratorUsersIndex,
            },
            created() {
                const vm = this;

                //vm.$bus.$on('event:return-bottle', (bottleId) => {
                //    vm.$refs.modalReturnBottle.open(bottleId);
                //});
            },
        }
    </script>