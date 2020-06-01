
<script>
    import pageMixin from '../../../Common/Mixins/pageMixin';

    export default {
        mixins: [pageMixin],
        methods: {
            toggleSidebar() {
                this.$bus.toggleSidebar();
            },
        }
    };
</script>
<style>
</style>