<template>
    <div v-cloak>
        <div v-if="filter.items.length">
            <div class="d-flex flex-column-reverse flex-sm-row justify-content-center justify-content-sm-between  align-items-center">
                <div class="small mt-2 mt-sm-0">
                    <div class="d-flex flex-row align-items-center">
                        <span class="mr-1 text-nowrap">
                            <span class="mr-1 font-weight-bold" v-text="filter.totalCount"></span>
                            record(s) in
                        </span>
                        <div class="d-flex flex-row align-items-center">
                            <select v-model="filter.pageSize" @change="changePagination1" class="custom-select custom-select-sm">
                                <option value="5">5</option>
                                <option value="10">10</option>
                                <option value="15">15</option>
                                <option value="25">25</option>
                                <option value="40">40</option>
                            </select>

                            <span class="ml-1">Pages</span>
                        </div>
                    </div>
                </div>
                <div class="">
                    <b-pagination v-model="filter.pageIndex"
                                  :total-rows="filter.totalCount"
                                  :per-page="filter.pageSize"
                                  aria-controls="my-table"
                                  v-on:change="changePagination2"
                                  limit="1"
                                  class="p-0 m-0">
                        <!--<template v-slot:page="f">
                        {{f.page}}/{{filter.totalPages}}
                    </template>-->
                    </b-pagination>
                </div>
            </div>

        </div>
        <div v-else>
            <div class="text-info font-weight-bold text-center">No record(s) found.</div>
        </div>

    </div>
</template>
<script>
    export default {
        props: {
            filter: Object,
            search: Function,
        },
        data() {
            return {
            };
        },
        methods: {
            changePagination1() {
                const vm = this;
                //if (page !== vm.filter.pageIndex)
                vm.search(1);
            },
            changePagination2(page) {
                const vm = this;
                if (page !== vm.filter.pageIndex)
                    vm.search(page);
            }
        }
    }
</script>