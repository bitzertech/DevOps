# Exercise 1

## Install kubernetes cluster

Using minikube is the easiest way to lean about Kubernetes.

Works with:

* Virtual box
* Docker for windows (don't let docker for windows install kubernetes)
* Hyper V

You can install minikube and kubectl with **Scoop** - a command line installer: https://scoop.sh/

When Scoop is installed, install minikube and kubectl with:
```shell
scoop install minikube
scoop install kubectl
```

### Install cluster

To install a cluster, type the following in e.g. powershell (open as admin):

```shell
$ minikube start --vm-driver=hyperv # or choose virtualbox, docker etc.
```

When it's done, you should enable ingress:

```shell
$ minikube addons enable ingress
```

Ingress is a load balancer object in Kubernetes, which also can assign external ip addresses to services.

## Check your configuration

View clusters and context's:

```shell
$ kubectl config view
```

To view nodes in the cluster, type:

```shell
$ kubectl get nodes
```

If you add the `-o wide` parameters to the above command, you will also see the public IP addresses of the nodes:

```shell
$ kubectl get nodes -o wide

NAME       STATUS   ROLES    AGE   VERSION   INTERNAL-IP     EXTERNAL-IP   OS-IMAGE              KERNEL-VERSION   CONTAINER-RUNTIME
minikube   Ready    master   27h   v1.17.3   172.17.253.26   <none>        Buildroot 2019.02.9   4.19.94          docker://19.3.6
```

>**Note:** On local kubernetes clusters, you'll see master and worker nodes. On clusters provided by a service provider, only worker nodes will be shown. Minikube only have a master node.

## Namespaces

Namespaces are the default way for kubernetes to separate resources.

### Create a namespace (optional)

Choose a name for your namespace.

```shell
$ kubectl create namespace my-namespace
namespace "my-namespace" created

$ kubectl get namespaces
```
You should now see your namespace created.

The default namespace for kubectl is 'default'.

```shell
$ kubectl get pods -n default
$ kubectl get pods
```

To overwrite the default namespace for your current `context`, run:

```shell
$ kubectl config set-context $(kubectl config current-context) --namespace=my-namespace
Context "<your current context>" modified.
```

You can verify that you've updated your current `context` by running:

```shell
kubectl config get-contexts
```

## Pods and Deployments

A **Pod** (*not container*) is the smallest building-block/worker-unit in Kubernetes, it has a specification of one or more containers and exists for the duration of the containers;

if all the containers stops or terminates, the Pod is stopped.

Usually a pod will be part of a **Deployment**; a more controlled or _robust_ way of running Pods.

A deployment can be configured to automatically delete stopped or exited Pods and start new ones, as well as run a number of identical Pods e.g. to provide high-availability.

### Create a pod

```shell
$ kubectl create deployment web --image=k8s.gcr.io/echoserver:1.4
deployment.apps/web created
```

The `create`-command is great for imperative testing, and getting something up and running fast.

It creates a _deployment_ named `web`, which creates a _replicaset_, which starts a _pod_ using the docker image `echoserver`.

Just so you know what we're talking about, you can check the objects you've created with `get <object>`, either one at a time, or all-together like below:

```bash
$ kubectl get deployment,replicaset,pod    # NB: no whitespace in the comma-separated list
```

> A ReplicaSet is something which deals with the number of copies of this pod.

### Testing access to our Pod (optional)

Run the following command to tunnel traffic (requires admin rights). This simulates the actual load balancer and routes traffic from our local maching into the cluster (exit by press ctrl + c):

```shell
$ minikube tunnel
```

To illustrate that we actually have a functioning web-server running in our pod, let's try exposing it to the internet and access it from a browser!

First use the following command to create a `service` for your `deployment`:

```shell
$ kubectl expose deployment web --port 8080 --type LoadBalancer
web exposed
```

Get the `service` called `web` and note down the LoadBalancer:

```shell
$ kubectl get service web
NAME  TYPE          CLUSTER-IP      EXTERNAL-IP   PORT(S)          AGE
web   LoadBalancer  10.96.223.218   10.96.223.218 8080:32458/TCP   12s
```

The external ip is a cluster ip and can't be accessed directly. The 'tunnel' command routes traffic from the 'minikube ip' into the cluster.

You can get the minikube ip by typing the command (needs admin rights):

```shell
$ minikube ip
172.17.221.78    # NB: your ip may be different
```

Now open a browser and point it to: 172.17.221.78:32458

Or open it directly in the browser by typing:

```shell
$ minikube service web -n my-namespace
```

### Cleanup

To clean up what we just deployed, start by removing the service:

```shell
$ kubectl delete service web
```

Then delete the deployment:

```shell
$ kubectl delete deployment web
```
