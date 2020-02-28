# Deploying our own MVC application on Kubernetes

We will build and deploy an MVC application on Kubernetes.

For the example we use an app called MyMovies. The application is avilable in the directory MyMovies and consists of a .Net Core 3.1 MVC application to register movies with title, instructor and year of publication.

The application uses a PostgreSQL database and self-signed SSL certificates.

For more background information and a step-by-step introduction to the creation of this application, follow the tutorial at ???

## Kubernetes parts in this demo

We need to define the following parts in a K8s setup:

1. A deployment for the database.
1. A service for the database.
1. A deployment for the MVC App.
1. A service for the MVC App.
1. An exposure for our application to the world around us.

A deployment describes a container that should be deployed in one or more instances.

A service describes an interface to a deployment to be used by other services or deployments.

The exposing of parts of our application is also called an Ingress for inbound traffic. The Ingress is only the description of which kind of traffic is accepted from the outside. The Ingress definition is working with an Ingress controller to route traffic from the outside to the intended services inside the kubernetes cluster. An Ingress controller is usually part of the infrastructure provided by the hosting provider, and not something handled by ouselves as developers.

SSL certificates also has to be handled by the Ingress in a Kubernetes setup.

These K8s parts could be defined in one collective file, or one file per part.
In this example there is one file for each part. To start with certificates, run two commands to register certificate as secrets:

Assuming that minikube is running, and that Ingress addon has been enabled as shown in the previous exercises, we are ready to start exercise 3.

## Exercise 3

* Making minikube and docker share environment.
* Register DNS name for kubernetes node.
* Create namespace for application.
* Building the application.
* Installing certificate.
* Installing deployments, services and ingress.
* Verify access to the application.
