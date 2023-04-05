<template>
    <div>
        <Header />
        <h1>This is Home</h1>
    </div>
</template>

<script>
import Header from "../components/Header.vue"

    export default {
      name: 'home',
      components: {
        Header
    }
    };
</script>