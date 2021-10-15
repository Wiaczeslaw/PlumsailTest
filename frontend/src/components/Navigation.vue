<template>
  <div>
    <router-link
        to="/"
        class="nav-item"
    ><strong class="link">list</strong></router-link>
  </div>
</template>

<script>
export default {
  name: "Navigation"
}
</script>

<style scoped>
.nav-item {
  padding-right: 10px;
}
.link {
  text-decoration: none;
}
</style>
