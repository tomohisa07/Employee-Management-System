<template>
    <nav class="navbar navbar-expand-sm bg-light navbar-dark">
        <ul class="navbar-nav">
        <li class="nav-item m-1">
            <router-link class="btn btn-light btn-outline-primary"
            to="/home">Home</router-link>
        </li>
        <li class="nav-item m-1">
            <router-link class="btn btn-light btn-outline-primary"
            to="/department">Department</router-link>
        </li>
        <li class="nav-item m-1">
            <router-link class="btn btn-light btn-outline-primary"
            to="/employee">Employee</router-link>
        </li>
        </ul>
    </nav>
</template>

<script>
export default {
  name: 'Header'
};
</script>