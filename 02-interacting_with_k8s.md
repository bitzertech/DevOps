# Interacting with Kubernetes

There are various ways to interact with kubernetes:

* kubectl - the low level command line tool
* Dashboard
* YAML files with resource definitions
* Helm - templating tool for semi-large applications
* Helmfile and Helmsman - helm chart management

## YAML

The following describes a pod via YAML:

```bash
apiVersion: v1
kind: Pod            # Defines a POD resource - all resources have a kind
metadata:
  name: frontend     # Resource is named 'frontend'
  labels:
    app: frontend    # Resources have labels. Labels connect everything in Kubernetes
spec:
  containers:        # This pod consist of two containers
  - name: webserver
    image: nginx
    ports:
    - containerPort: 80
  - name: myapp
    image: nodeapp
```

The same would be in docker command for a single container:

```shell
$ docker run -d --restart=always --name webserver -p 80:80 nginx
```

and in kubectl:

```shell
$ kubectl create webserver --image=nginx --port=80
```

### Labels

Labels are important in kubernetes, it ties everything together:

* Everything can have labels
* Labels are arbitrary key-values
* Labels are the glue between most resources
* Using multiple labels allows for an N-dimensional grouping of resources

### A deployment

```bash
apiVersion: apps/v1
kind: Deployment     # 
metadata:
  name: frontend     # Resource is named 'frontend'
spec:
  selector:
    matchLabels:
      app: nginx
  replicas: 4        # We want 4 replicas - Kubernetes will deploy and manage these for us
  template:
    metadata:        # Rest is based on the pod template
      labels:
        app: frontend
      spec:
        containers:
        - name: webserver
          image: nginx
          ports:
          - containerPort: 80
        - name: myapp
          image: nodeapp
```

and the service:

```bash
apiVersion: v1
kind: Service
metadata:
  labels:
    app: nginx       # label for this service
  name: nginx        # name of service
spec:
  ports:
  - port: 80
    targetPort: 80
  selector:
    app: nginx       # the deployment to map this service to
```

To sum up:

![Namespaces](Images/k8s-manage-depl.png "Namespaces")

With deployments you can:

* Create new deployments
* Update existing deployments
* Rollback deployments
* Scale a deployment
* Pause or resume a deployment

## Exercise 2

* Create a YAML deployment file for the echo server from exercise 1.
* Create a YAML service file for the echo server.
* Verify access to the server.

To deploy a yaml file, the following command is used:

```bash
$ kubectl create/apply/delete -f my-deployment.yaml  # you can add '--dry-run=true' to let kubectl verify the file
```
