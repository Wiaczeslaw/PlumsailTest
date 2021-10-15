<template>
  <div id="app">
    <div>
      <Navigation class="nav-bar"></Navigation>
      <router-view />
    </div>
  </div>
</template>

<script>
import Navigation from './components/Navigation';

export default {
  components: {
    Navigation
  },
}
</script>

<style>
#app {
  justify-content: center;
  height: 100vh;
  position: fixed;
  top: 10px;
  bottom: 0;
  left: 0;
  right: 0;
  display: flex;
  margin-top: 20px;
}
.nav-bar {
  padding-bottom: 50px;
}
</style>
