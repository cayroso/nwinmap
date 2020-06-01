<style></style>
<template>
    <div v-cloak>
        <div class="row">
            <div class="col-sm-6">
                <div class="card">
                    <div class="card-body">
                        <div class="form-horizontal">
                            <div v-if="validations.size > 0" class="alert alert-danger">
                                <ul>
                                    <li v-for="item in validations">
                                        {{item[1]}}
                                    </li>
                                </ul>
                            </div>

                            <div class="">

                                <div class="form-group row">
                                    <label class="col-sm-3 text-sm-right control-label">Barangay</label>
                                    <div class="col">
                                        <b-form-select v-model="item.barangayId" :options="barangays" class="custom-select">
                                        </b-form-select>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-3 text-sm-right control-label">Type</label>
                                    <div class="col">
                                        <b-form-select v-model="item.itemType">
                                            <b-form-select-option value="1">Camera</b-form-select-option>
                                            <b-form-select-option value="2">Wifi</b-form-select-option>
                                        </b-form-select>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-3 text-sm-right control-label">Status</label>
                                    <div class="col">
                                        <b-form-select v-model="item.itemStatus">
                                            <b-form-select-option value="-1">Unknown</b-form-select-option>
                                            <b-form-select-option value="0">Off</b-form-select-option>
                                            <b-form-select-option value="1">On</b-form-select-option>
                                        </b-form-select>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-3 text-sm-right control-label">Name</label>
                                    <div class="col">
                                        <input v-model="item.name" class="form-control" required />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-3 text-sm-right control-label">Description</label>
                                    <div class="col">
                                        <input v-model="item.description" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-3 text-sm-right control-label">Source</label>
                                    <div class="col">
                                        <textarea v-model="item.source" class="form-control" rows="3"></textarea>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <label class="col-sm-3 text-sm-right control-label">Address</label>
                                    <div class="col">
                                        <textarea v-model="item.address" class="form-control" rows="3" readonly required></textarea>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class=" col-sm-9 offset-3">
                                        <button @click="save" class="btn btn-primary">
                                           <span class="fas fa-fw fa-save mr-1"></span>Save
                                        </button>
                                        <button @click="close" class="btn btn-secondary">
                                            <span class="fas fa-fw fa-times mr-1"></span>Cancel
                                        </button>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm">
                <g-map-add-item map-name="map1" :cx.sync="item.latitude" :cy.sync="item.longitude" @onAddress="onAddress" ref="gmap" @ready="getItem" style="height:600px;width:100%;">
                    Google map should be here
                </g-map-add-item>
            </div>
        </div>
    </div>
</template>
<script>
    import pageMixin from '../../../Common/Mixins/pageMixin';
    export default {
        mixins: [pageMixin],
        props: {
            id: String,
            uid: String,
            durl: String,
            rurl: String
        },
        data() {
            return {
                validations: new Map(),

                barangays: [],
                item: {
                    barangayId: null,
                    name: null,
                    itemType: null,
                    itemStatus: null,
                    description: null,
                    source: null,
                    address: null,
                    latitude: 13.846569,
                    longitude: 120.989290
                }
            };
        },
        async created() {
            const vm = this;

            await vm.getBarangays();

            //await vm.getItem();

        },
        methods: {

            async getItem() {
                const vm = this;

                if (!vm.id)
                    return;

                await vm.$util.axios.get(`api/admin/items/${vm.id}`)
                    .then(resp => {

                        vm.item = resp.data;

                        vm.$refs.gmap.setCenter(vm.item.latitude, vm.item.longitude);
                    })
            },

            async getBarangays() {
                const vm = this;
                await vm.$util.axios.get(`api/admin/barangays`)
                    .then(resp => {
                        vm.barangays = resp.data;
                    })
            },

            isFormValid() {
                const vm = this;
                const item = vm.item;
                const vals = new Map();

                if (!item.barangayId)
                    vals.set('barangayId', 'Barangay is required.');
                if (!item.name)
                    vals.set('name', 'Name is required.');
                if (!item.address)
                    vals.set('address', 'Address is required.');

                vm.validations = vals;

                return vals.size === 0;
            },

            async save() {
                const vm = this;

                if (!vm.isFormValid())
                    return;

                const payload = vm.$util.clone(vm.item);

                if (vm.id) {
                    try {

                        payload.itemId = vm.id;

                        await vm.$util.axios.put(`api/admin/items`, payload)
                            .then(resp => {
                                alert('updated');
                                vm.close();
                            });
                    } catch (e) {
                        vm.$util.handleError(e);
                    }
                }
                else {
                    try {
                        await vm.$util.axios.post(`api/admin/items`, payload)
                            .then(resp => {
                                alert('created');
                                vm.close();;
                            });
                    } catch (e) {
                        vm.$util.handleError(e);
                    }
                }

            },

            async onItemMarkerClicked(item) {
                const vm = this;

                //await vm.$util.axios.get(`api/customers/stores/${store.tenantId}`)
                //    .then(resp => {
                vm.item = item;
                //vm.store.products = resp.data;

                vm.showItemInfo = true;
                //});
            },
            onAddress(address, location) {
                const item = this.item;

                item.address = address.formatted_address;
                item.latitude = location.lat;
                item.longitude = location.lng;
            },

        }
    }
</script>