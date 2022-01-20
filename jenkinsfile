pipeline {
  agent {docker {image 'mono:6.12.0.122-slim'}}
  stages {
    stage('build'){
      steps {
        sh 'dotnet --version'
      }
    }
