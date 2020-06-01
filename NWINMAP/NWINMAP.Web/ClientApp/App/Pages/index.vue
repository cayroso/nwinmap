<style></style>
<template>
    <div v-cloak class="h-100">

        <div class="h-100">
            <!--13.846569, 120.989290-->
            <g-map :items="items" map-name="map1" :cx="13.846569" :cy="120.989290" @onItemMarkerClicked="onItemMarkerClicked" @onAddress="onAddress" ref="gmap" style="height:800px;width:100%;">
                Google map should be here
            </g-map>
        </div>

        <b-modal ref="modalShowItem"
                 no-close-on-backdrop
                 scrollable>
            <template v-slot:modal-header>
                <h1 class="h4 mb-0 card-title">Device</h1>
            </template>
            <template v-slot:modal-footer>
                <button v-if="uid" @click="edit(item)" class="btn btn-primary">
                    <span class="fas fa-fw fa-edit mr-1"></span>Edit
                </button>
                <button @click="$refs.modalShowItem.hide()" class="btn btn-secondary">
                    <span class="fas fa-fw fa-times mr-1"></span>Cancel
                </button>
            </template>
            <div>
                <div v-if="item">

                    <div class="form-group row mb-sm-0">
                        <label class="col-3 text-right col-form-label">Barangay</label>
                        <div class="col">
                            <span class="form-control-plaintext">{{item.barangay.name}}</span>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Information</legend>

                        <div class="form-group row mb-sm-0">
                            <label class="col-3 text-right col-form-label">Name</label>
                            <div class="col">
                                <span class="form-control-plaintext">{{item.name}}</span>
                            </div>
                        </div>
                        <div class="form-group row mb-sm-0">
                            <label class="col-3 text-right col-form-label">Type</label>
                            <div class="col">
                                <span class="form-control-plaintext"><span v-bind:class="itemTypeIcon" class="mr-1"></span>{{item.itemTypeText}}</span>
                            </div>
                        </div>
                        <div class="form-group row mb-sm-0">
                            <label class="col-3 text-right col-form-label">Status</label>
                            <div class="col">
                                <span class="form-control-plaintext"><span v-bind:class="itemStatusIcon" class="mr-1"></span>{{item.itemStatusText}}</span>
                            </div>
                        </div>
                        <div class="form-group row mb-sm-0">
                            <label class="col-3 text-right col-form-label">Description</label>
                            <div class="col">
                                <span class="form-control-plaintext">{{item.description}}</span>
                            </div>
                        </div>
                        <div class="form-group row mb-sm-0">
                            <label class="col-3 text-right col-form-label">Address</label>
                            <div class="col">
                                <span class="form-control-plaintext">{{item.address}}</span>
                            </div>
                        </div>
                        <div v-if="item.image" class="form-group row mb-sm-0">
                            <label class="col-3 text-right col-form-label">Image</label>
                            <div class="col">
                                <span class="form-control-plaintext">Image placeholder</span>
                            </div>
                        </div>
                        <div v-if="item.source" class="form-group row mb-sm-0">
                            <label class="col-3 text-right col-form-label">Source</label>
                            <div class="col">
                                <span class="form-control-plaintext">{{item.source}}</span>
                            </div>
                        </div>
                    </fieldset>

                    <fieldset>
                        <legend>Device Administrator</legend>
                        <div class="form-group row mb-sm-0">
                            <label class="col-3 text-right col-form-label">Name</label>
                            <div class="col">
                                <span class="form-control-plaintext">{{item.user.lastName}}, {{item.user.firstName}} </span>
                            </div>
                        </div>
                        <div class="form-group row mb-sm-0">
                            <label class="col-3 text-right col-form-label">Email</label>
                            <div class="col">
                                <span class="form-control-plaintext">{{item.user.email}} </span>
                            </div>
                        </div>
                        <div class="form-group row mb-sm-0">
                            <label class="col-3 text-right col-form-label">Phone</label>
                            <div class="col">
                                <span class="form-control-plaintext">{{item.user.phoneNumber}} </span>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </b-modal>

    </div>
</template>
<script>
    export default {
        props: {
            uid: String,
            durl: String,
            rurl: String,
            urlEdit: String
        },
        data() {
            return {
                items: [],

                showItemInfo: false,
                item: null
            };
        },
        computed: {
            itemTypeIcon() {
                const vm = this;

                if (vm.item.itemType === 1)
                    return 'fas fa-fw fa-video';

                if (vm.item.itemType === 2)
                    return 'fas fa-fw fa-wifi';

                return 'fas fa-fw fa-exclamation-triangle text-danger';
            },

            itemStatusIcon() {
                const vm = this;

                if (vm.item.itemStatus === 0)
                    return 'fas fa-fw fa-toggle-off text-warning';

                if (vm.item.itemStatus === 1)
                    return 'fas fa-fw fa-toggle-on text-success';

                return 'fas fa-fw fa-exclamation-triangle text-danger';
            },
        },
        async created() {
            const vm = this;

            await vm.getItems();
        },
        methods: {

            async getItems() {
                const vm = this;
                await vm.$util.axios.get(`api/admin/items`)
                    .then(resp => {
                        vm.items = resp.data;
                    })
            },

            async onItemMarkerClicked(item) {
                const vm = this;

                //await vm.$util.axios.get(`api/customers/stores/${store.tenantId}`)
                //    .then(resp => {
                vm.item = item;
                //vm.store.products = resp.data;

                vm.$refs.modalShowItem.show();
                //vm.showItemInfo = true;
                //});
            },
            onAddress() {
                this.showItemInfo = true;
            },

            edit(item) {
                const vm = this;

                const href = `${vm.urlEdit}${item.itemId}`;

                vm.$util.href(href);
            }
        }
    }
</script>