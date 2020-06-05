<style></style>
<template>
    <div v-cloak>
        <div class="d-flex flex-column flex-sm-row justify-content-between align-items-sm-center">
            <div>
                <h1 class="h3 mb-sm-0">
                    <span class="fas fa-fw fa-users mr-1"></span>Users
                </h1>
            </div>
            <div class="d-flex flex-row justify-content-end">

                <div>

                    <div class="input-group">
                        <!--<div class="input-group-prepend">
                            <button @click="filter.visible = !filter.visible" class="btn btn-secondary">
                                <span class="fas fa-fw fa-filter"></span>
                            </button>
                        </div>-->
                        <input v-model="criteria" @keyup.enter="search" type="text" class="form-control">
                        <div class="input-group-append">
                            <button @click="search" class="btn btn-secondary" type="button" id="button-addon2">
                                <span class="fa fas fa-fw fa-search"></span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <b-overlay :show="busy">
            <div class="table-responsive mb-sm-0 mt-2 ">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <td>#</td>
                            <td>User</td>
                            <td>Roles</td>
                            <td>Barangay</td>
                            <td style="width:55px;">
                                Action
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item,i) in items" :key="item.userId">
                            <td>{{i+1}}</td>
                            <td>
                                <div class="form-group row mb-sm-0">
                                    <label class="col-sm-3 text-md-right col-form-label">Name</label>
                                    <div class="col-sm">
                                        <span class="form-control-plaintext"> {{item.firstName}} {{item.lastName}}</span>
                                    </div>
                                </div>
                                <div class="form-group row mb-sm-0">
                                    <label class="col-sm-3 text-md-right col-form-label">Email</label>
                                    <div class="col-sm">
                                        <span class="form-control-plaintext"> {{item.email}}</span>
                                    </div>
                                </div>
                                <div class="form-group row mb-sm-0">
                                    <label class="col-sm-3 text-md-right col-form-label">Phone</label>
                                    <div class="col-sm">
                                        <span class="form-control-plaintext"> {{item.phone}}</span>
                                    </div>
                                </div>
                            </td>
                            <td>
                                <ul>
                                    <li v-for="r in item.roles" :key="r.roleId">
                                        {{r.name}}
                                    </li>
                                </ul>

                            </td>
                            <td>
                                
                                <ul>
                                    <li v-for="r in item.barangayUserRoles" :key="r.barangayId">
                                        {{r.barangayName}}
                                    </li>
                                </ul>

                            </td>
                            <td>
                                <div class="d-flex flex-column justify-content-center align-content-center">
                                    <button @click="openEditRole(item)" class="btn btn-primary mb-1">Manage Role</button>
                                    <button @click="openEditBarangay(item)" class="btn btn-primary">Manage Barangay</button>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

        </b-overlay>

        <b-modal ref="modalEditRole"
                 no-close-on-backdrop
                 scrollable>
            <template v-slot:modal-header>
                <h1 class="h4 mb-0 card-title">
                    Manage Role
                </h1>
            </template>
            <template v-slot:modal-footer>
                <button @click="saveRole" class="btn btn-primary">Save</button>
                <button @click="closeEditRole" class="btn btn-secondary">Cancel</button>
            </template>
            <div v-if="selectedUser">
                <div class="form-group row">
                    <label class="col-sm-4 text-md-right col-form-label">First Name</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{selectedUser.firstName}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-md-right col-form-label">Last Name</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{selectedUser.lastName}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-md-right col-form-label">Email</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{selectedUser.email}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-md-right col-form-label">Phone</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{selectedUser.phone}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-md-right col-form-label">Roles</label>
                    <div class="col-sm">
                        <b-form-checkbox-group v-model="selectedRoles"
                                               :options="roles"
                                               value-field="roleId"
                                               text-field="name"
                                               stacked></b-form-checkbox-group>
                    </div>
                </div>


            </div>
        </b-modal>

        <b-modal ref="modalEditBarangay"
                 no-close-on-backdrop
                 scrollable>
            <template v-slot:modal-header>
                <h1 class="h4 mb-0 card-title">
                    Assing/Remove Barangay
                </h1>
            </template>
            <template v-slot:modal-footer>
                <button @click="saveBarangay" class="btn btn-primary">Save</button>
                <button @click="closeEditBarangay" class="btn btn-secondary">Cancel</button>
            </template>
            <div v-if="selectedUser">
                <div class="form-group row">
                    <label class="col-sm-4 text-md-right col-form-label">First Name</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{selectedUser.firstName}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-md-right col-form-label">Last Name</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{selectedUser.lastName}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-md-right col-form-label">Email</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{selectedUser.email}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-md-right col-form-label">Phone</label>
                    <div class="col-sm">
                        <span class="form-control-plaintext">{{selectedUser.phone}}</span>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-4 text-md-right col-form-label">Barangays</label>
                    <div class="col-sm">
                        <b-form-checkbox-group v-model="selectedBarangays"
                                               :options="barangays"
                                               value-field="barangayId"
                                               text-field="name"
                                               stacked></b-form-checkbox-group>
                    </div>
                </div>
            </div>
        </b-modal>
    </div>
</template>
<script>
    import pageMixin from '../../../../Common/Mixins/pageMixin';

    export default {
        mixins: [pageMixin],
        props: {
            uid: String,
            durl: String,
            rurl: String
        },
        data: () => ({
            roles: [],
            barangays: [],

            criteria: '',
            items: [],

            selectedUser: null,
            selectedRoles: [],
            selectedBarangays: [],
        }),
        computed: {
            
        },

        async created() {
            const vm = this;

            await vm.search();
            await vm.getRoles();
            await vm.getBarangays();
        },
        methods: {
            async getRoles() {
                const vm = this;

                try {
                    await vm.$util.axios.get(`api/admin/roles`)
                        .then(resp => {
                            vm.roles = resp.data;
                        });
                } catch (e) {
                    vm.$util.handleError(e);
                }

            },

            async getBarangays() {
                const vm = this;

                try {
                    await vm.$util.axios.get(`api/admin/barangays`)
                        .then(resp => {
                            vm.barangays = resp.data;
                        });
                } catch (e) {
                    vm.$util.handleError(e);
                }

            },

            async search() {
                const vm = this;
                const filter = vm.filter;

                if (vm.busy)
                    return;

                const query = [
                    '?c=', encodeURIComponent(vm.criteria),
                ].join('');

                try {
                    vm.busy = true;

                    await vm.$util.axios.get(`api/admin/users/${query}`)
                        .then(resp => {
                            vm.items = resp.data;

                            vm.items.forEach(item => {

                                vm.$set(item, 'expand', false);
                            });
                        })
                } catch (e) {
                    vm.$util.handleError(e);
                } finally {
                    vm.busy = false;
                }

            },

            openEditRole(user) {
                const vm = this;

                vm.selectedUser = user;
                vm.selectedRoles = user.roles.map(e => e.roleId);
                vm.$refs.modalEditRole.show();
            },
            async saveRole() {
                const vm = this;
                const user = vm.selectedUser;

                try {
                    //  to add
                    const roleIdsAdd = [];
                    const roleIdsRemove = [];

                    vm.selectedRoles.forEach(r => {

                        let found = user.roles.find(ur => ur.roleId == r);

                        if (!found)
                            roleIdsAdd.push(r);
                    })

                    user.roles.forEach(ur => {

                        let found = vm.selectedRoles.find(r => r == ur.roleId);

                        if (!found)
                            roleIdsRemove.push(ur.roleId);
                    });

                    //  to remove
                    const payload = {
                        userId: user.userId,
                        roleIdsAdd,
                        roleIdsRemove
                    };

                    await vm.$util.axios.post(`api/admin/manage-user-role`, payload)
                        .then(resp => {
                            vm.search();

                            vm.$bvToast.toast(`User's roles updated`, { title: 'Manage User Roles', variant: 'success', toaster: 'b-toaster-bottom-right' });

                            vm.closeEditRole();
                        });

                } catch (e) {
                    vm.$util.handleError(e);
                }
            },
            closeEditRole() {
                const vm = this;

                vm.selectedUser = null;
                vm.selectedRoles = [];
                vm.$refs.modalEditRole.hide();
            },

            openEditBarangay(user) {
                const vm = this;

                vm.selectedUser = user;
                vm.selectedBarangays = user.barangayUserRoles.map(e => e.barangayId);
                vm.$refs.modalEditBarangay.show();

            },
            async saveBarangay() {
                const vm = this;
                const user = vm.selectedUser;

                try {
                    //  to add
                    const barangayIdsAdd = [];
                    const barangayIdsRemove = [];

                    vm.selectedBarangays.forEach(r => {

                        let found = user.barangayUserRoles.find(ur => ur.barangayId == r);

                        if (!found)
                            barangayIdsAdd.push(r);
                    })

                    user.barangayUserRoles.forEach(ur => {

                        let found = vm.selectedBarangays.find(r => r == ur.barangayId);

                        if (!found)
                            barangayIdsRemove.push(ur.barangayId);
                    });

                    //  to remove
                    const payload = {
                        userId: user.userId,
                        barangayIdsAdd,
                        barangayIdsRemove
                    };
                    debugger;
                    await vm.$util.axios.post(`api/admin/manage-user-barangay`, payload)
                        .then(resp => {
                            vm.search();

                            vm.$bvToast.toast(`User's assigned barangay updated`, { title: 'Manage User Barangay', variant: 'success', toaster: 'b-toaster-bottom-right' });

                            vm.closeEditBarangay();
                        });

                } catch (e) {
                    vm.$util.handleError(e);
                }
            },
            closeEditBarangay() {
                const vm = this;

                vm.selectedUser = null;
                vm.selectedBarangays = [];
                vm.$refs.modalEditBarangay.hide();
            },
        }
    }
</script>